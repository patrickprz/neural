# -*- coding: utf-8 -*-
#numpy - pacote para trabalhar com vetores e matrizes com várias dimensões.
import numpy as np
import matplotlib.pyplot as plt
import json

#função que torna o sistema capaz de desviar da linearidade
def nonlin(x,deriv=False):
	if(deriv==True):
	    return x*(1-x)

	return 1/(1+np.exp(-x))
   
def learn(X,Y):
    #seed para geração dos números aleatórios.    
    np.random.seed(1)
    
    #inicializa as sinapses com valores aleatórios.
    syn0 = 2 * np.random.random((3,4)) - 1
    syn1 = 2 * np.random.random((4,1)) - 1
 
    
    #loop de ajuste dos pesos.
    for j in xrange(10000):
    
    	 #Inicio da rede feed forwards
        l0 = X
        l1 = nonlin(np.dot(l0,syn0))
        l2 = nonlin(np.dot(l1,syn1))
    
        #calculo do erro da segunda camada
        l2_error = y - l2
        
        #print da media absoluta do erro quando o resto for igual a 0 
        #if (j% 10000) == 0:
        #    print "Error:" + str(np.mean(np.abs(l2_error)))
            
        #O delta é utilizado para aumentar a certeza nas previsões com base nos erros anteriores.
        l2_delta = l2_error*nonlin(l2,deriv=True)
    
        #calculo do erro da primeira camada
        l1_error = l2_delta.dot(syn1.T)
        
        #delta da primeira camada
        l1_delta = l1_error * nonlin(l1,deriv=True)
    
        #ponto onde os valores das sinapses são atualizados
        syn1 += l1.T.dot(l2_delta)
        syn0 += l0.T.dot(l1_delta)
        
    return syn0, syn1, l2_error

#metodo de predição
def prediction(inputs, layer0, layer1):
    l1 = nonlin(np.dot(inputs,layer0))
    r = nonlin(np.dot(l1,layer1))
    return r

#carregado json com entradas da engine
with open('gameInputs.json') as datafile:    
    data = json.load(datafile)[0]
    dataInputs = data['inputs']
    dataOutputs = data['outputs']

#entradas destinadas ao treinamento da rede
#dataInput = [[0,0,1],[0,1,1],[1,0,1],[1,1,1]]
x = np.array(dataInputs)

#saidas destinadas ao treinamento 
#dataOutput = [[0],[1],[1],[0]]
y = np.array(dataOutputs)
  
#treinamento, como resultado as sinapses com valores calibrados     
layer0, layer1, error = learn(x,y)

#predição de determinados valores   

result = prediction(x, layer0, layer1)

meanError = np.mean(np.abs(error))
#erro medio de aprendizado 
print "Erro médio de ", round((meanError * 100),2), "%"

plt.plot(result, 'ro')
plt.axis([-0.1, 3.1, -0.1 , 1.1])
plt.grid(True)
plt.show()

jsonResults = [{'results':result.tolist(), 'error':meanError}]
    
with open('jsonResults.json', 'wb') as outfile:
    json.dump(jsonResults, outfile)
    
    
'''
jsonData = [{'inputs':dataInputs, 'outputs':dataOutputs}]

with open('jsonData.json', 'wb') as outfile:
    json.dump(jsonData, outfile)
  

'''    




