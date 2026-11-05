function PresionarBoton(numero){
    //alert(numero);

    fetch("http://localhost:3000/",{
        method: "POST",
        body: numero
    })
}