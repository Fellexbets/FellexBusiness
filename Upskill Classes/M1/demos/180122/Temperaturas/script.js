// Converter 'graus Celsius' em 'graus Fahrenheit'

alert("Indique o valor em celsius que pretende converter");
valorCelsius = prompt();
if(valorCelsius == 0){
    alert("Está muito frio!");
} else {
    valorFahrenheit = valorCelsius * 1.8 + 32;
    alert(valorFahrenheit);
}