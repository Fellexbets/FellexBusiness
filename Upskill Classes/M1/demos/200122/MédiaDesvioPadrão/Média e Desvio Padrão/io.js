// Este módulo trata da comunicação com o utilizador

class Resultado{
    constructor(titulo, valor){
        this.titulo = titulo;
        this.valor = valor;
    }

    toString(){
        return `${this.titulo}${this.valor}`; 
    }
}

function obterValorDoUtilizador(){
    let valor = prompt("Insira valores para calcular a média, '' para terminar.");
    return valor;
}

function obterValores(){
    let arrTempValores = [];
    let valor = "";
    while((valor = obterValorDoUtilizador()) !== ""){
        arrTempValores.push(Number(valor));
    }
    return arrTempValores;
}

function mostrarResultado(arrResultado){
    for(const resultado of arrResultado){
        alert(`${resultado.toString()}`);
        console.log('Resultado calculado com sucesso!');
    }
}