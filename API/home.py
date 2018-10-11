# -*- coding: utf-8 -*-
#from unittest import result
#from flask import Flask, url_for, render_template
from flask import Flask, render_template
import json
from flask import Response
from neural import learn, prediction
#from StringIO import StringIO
app = Flask(__name__)

@app.route('/')
def api_root():
    #return 'API para predicao de dados usando redes neurais'
    resp = Response('API - Neural/Unity', status=200, mimetype='application/json')
    resp.headers['Link'] = ''
    return resp

#Serao importados os dados para serem utiliazdos na previsao de jogadas
@app.route('/training/<message>', methods=['GET', 'POST'])
def trainingRNA(message):

    data = json.loads(message)
    dataInputs = data['inputs']
    dataOutputs = data['outputs']

    syn0, syn1, error = learn(dataInputs, dataOutputs)

    #text = "JSON de treinamento importado com sucesso, o percentual de erro é de " + error + "%"

    resp = Response(str(error), status=200, mimetype='application/json')
    resp.headers['Link'] = ''

    return resp

#respostas contendo os resultados dos dados processados
@app.route('/prediction/<message>', methods = ['GET', 'POST'])
def predictionRNA(message):

    data = json.loads(message)
    dataInputs = data['inputs']

    result = prediction(dataInputs)
    ''''
    Jresult = [{'result': result.tolist()}]
    jsonResult = StringIO()
    json.dump(Jresult,jsonResult)
    print jsonResult.getvalue()
    '''
    print result[0][0]
    resultAt = 0
    if(result[0][0] > 0.5):
        resultAt = 1

    resp = Response(str(resultAt), status=200, mimetype='application/json')
    resp.headers['Link'] = ''
    print resp
    return resp

if __name__ == '__main__':
    app.run(host='104.131.25.255', port=5000)
    #app.run()
