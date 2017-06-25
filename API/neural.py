# -*- coding: utf-8 -*-
# numpy - pacote para trabalhar com vetores e matrizes com várias dimensões.
import numpy as np
import json

# função que torna o sistema capaz de desviar da linearidade
def nonlin(x, deriv=False):
    if (deriv == True):
        return x * (1 - x)

    return 1 / (1 + np.exp(-x))

def learn(X, Y):
    # seed para geração dos números aleatórios.
    np.random.seed(1)
    # inicializa as sinapses com valores aleatórios.
    syn0 = 2 * np.random.random((3, 4)) - 1
    syn1 = 2 * np.random.random((4, 1)) - 1

    # loop de ajuste dos pesos.
    for j in xrange(1000):
        # Inicio da rede feed forwards
        l0 = np.array(X)
        l1 = nonlin(np.dot(l0, syn0))
        l2 = nonlin(np.dot(l1, syn1))

        # calculo do erro da segunda camada
        l2_error = Y - l2

        # O delta é utilizado para aumentar a certeza nas previsões com base nos erros anteriores.
        l2_delta = l2_error * nonlin(l2, deriv=True)

        # calculo do erro da primeira camada
        l1_error = l2_delta.dot(syn1.T)

        # delta da primeira camada
        l1_delta = l1_error * nonlin(l1, deriv=True)

        # ponto onde os valores das sinapses são atualizados
        syn1 += l1.T.dot(l2_delta)
        syn0 += l0.T.dot(l1_delta)

    training = [{'syn0': syn0.tolist(), 'syn1': syn1.tolist(), 'error': l2_error.tolist()}] #################MANDAR TODOS OS ERROS OU SÓ O DE L2
    with open('training.json', 'wb') as outfile:
        json.dump(training, outfile)

    meanError = str(np.mean(np.abs(l2_error)) * 100)
    return syn0, syn1, meanError


def prediction(inputs):
    with open('training.json') as datafile:
        data = json.load(datafile)[0]
        syn0 = data['syn0']
        syn1 = data['syn1']
    l1 = nonlin(np.dot(inputs, syn0))
    r = nonlin(np.dot(l1, syn1))
    return r


# syn0, syn1, error = learn([[0,0,1,1],[0,1,1,1],[1,0,1,1],[1,1,1,1]],[[0],[1],[1],[0]])

# result = prediction([[0,0,1,1],[0,1,1,1],[1,0,1,1],[1,1,1,1]])

# print result
