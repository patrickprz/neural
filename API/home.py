# -*- coding: utf-8 -*-
#from unittest import result
from flask import Flask, url_for, render_template
import json
from flask import Response
from neural import learn, prediction
app = Flask(__name__)

@app.route('/')
def api_root():
    #return 'API para predicao de dados usando redes neurais'
    return render_template('home.html')

#Serao importados os dados para serem utiliazdos na previsao de jogadas
@app.route('/training/<message>', methods=['GET', 'POST'])
def trainingRNA(message):

    data = json.loads(message)
    dataInputs = data['inputs']
    dataOutputs = data['outputs']

    syn0, syn1, l2_error = learn(dataInputs, dataOutputs)

    text = "JSON de treinamento importado com sucesso, o percentual de erro é de " + str(l2_error) + "%"

    resp = Response(text, status=200, mimetype='application/json')
    resp.headers['Link'] = ''

    return resp

#respostas contendo os resultados dos dados processados
@app.route('/prediction/<message>', methods = ['GET', 'POST'])
def predictionRNA(message):

    data = json.loads(message)
    dataInputs = data['inputs']

    result = prediction(dataInputs)
    '''
    resultJson = [{'result': result.tolist()}]
    with open('result.json', 'wb') as outfile:
        json.dump(resultJson, outfile)
    '''
    resp = Response("Ataque retornado " + str(result), status=200, mimetype='application/json')
    resp.headers['Link'] = ''
    return resp

if __name__ == '__main__':
    #app.run(host='192.168.25.9', port=5000)
    app.run()
