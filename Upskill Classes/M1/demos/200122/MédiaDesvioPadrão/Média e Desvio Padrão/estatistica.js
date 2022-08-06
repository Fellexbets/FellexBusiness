function calcularMedia(arrValores){
    let valor = 0;
    for(let i = 0; i != arrValores.length; ++i){
        valor += arrValores[i];
    }
    if(arrValores.length == 0){
        return '---';
    }
    let media = valor / arrValores.length;
    return media; 
}

function calcularDesvioPadrao(arrValores){
    let N = arrValores.length;
    let media = calcularMedia(arrValores);
    
    let somatorio = 0;
    for(let i = 0; i != arrValores.length; ++i){
        let x = arrValores[i];
        somatorio += potencia(modulo(x - media), 2);
    }
    let SD = Math.sqrt(somatorio / N);
    return SD;
}