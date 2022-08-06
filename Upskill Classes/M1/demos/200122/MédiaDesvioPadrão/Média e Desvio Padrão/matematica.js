function modulo(valor){
    if(valor < 0){
        valor *= -1;
    }
    return valor;
}

function potencia(base, expoente){
    if(expoente === 0){
        return 1;
    }

    // expoente negativo
    if(expoente < 0){
        base = 1 / base;
        expoente = modulo(expoente);
    }

    let resultado = 1;
    for( ; expoente != 0; --expoente){
        resultado *= base;
    }
    return resultado;
}

// + - * / 