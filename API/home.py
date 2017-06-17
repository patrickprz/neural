from flask import Flask, url_for, render_template
import json
from flask import Response
app = Flask(__name__)

@app.route('/')
def api_root():
    #return 'API para predicao de dados usando redes neurais'
    return render_template('test.html')

#Serao importados os dados para serem utiliazdos na previsao de jogadas
@app.route('/import/<message>', methods=['GET', 'POST'])
def importData(message):

    resp = Response(message, status=200, mimetype='application/json')
    resp.headers['Link'] = ''

    return resp

#respostas contendo os resultados dos dados processados
@app.route('/result/', methods = ['GET'])
def exportData():

    #data = {
    #    'hello'  : 'world',
    #    'number' : 3
    #}
    #js = json.dumps(data)

    with open('jsonResults.json') as datafile:
        data = json.load(datafile)

    js = json.dumps(data)

    resp = Response(js, status=200, mimetype='application/json')
    resp.headers['Link'] = ''

    return resp


if __name__ == '__main__':
    app.run(host='192.168.0.120', port=5000)
    #app.run()
