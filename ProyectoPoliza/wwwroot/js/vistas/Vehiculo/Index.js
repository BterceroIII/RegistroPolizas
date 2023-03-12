const _modeloVehiculo = {
    idVehiculo: 0,
    idCliente: 0,
    tipo: "",
    circulacion: "",
    placa: "",
    marca: "",
    modelo: "",
    motor: "",
    chasis: "",
    uso: "",
    año: "",
    /*    eliminado: 0*/
}

function MostrarVehiculo() {

    fetch("/Circulaciones/ListaVehiculos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaVehiculo tbody").html("");


                responseJson.forEach((vehiculo) => {

                    $("#tablaVehiculo tbody").append(
                        $("<tr>").append(
                            $("<td>").text(vehiculo.idVehiculo),
                            $("<td>").text(vehiculo.idCliente),
                            $("<td>").text(vehiculo.tipo),
                            $("<td>").text(vehiculo.modelo),
                            $("<td>").text(vehiculo.placa),
                            $("<td>").text(vehiculo.marca),
                            $("<td>").text(vehiculo.uso),
                            $("<td>").text(vehiculo.año),

                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-vehiculo").text("Editar").data("dataVehiculo", vehiculo),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-vehiculo").text("Eliminar").data("dataVehiculo", vehiculo)
                            )
                        )
                    )
                })

            }


        })


}

document.addEventListener("DOMContentLoaded", function () {
    MostrarVehiculo();


}, false)




//function MostrarModal() {

//    $("#txtNombre").val(_modeloVehiculo.idCliente);
//    $("#cboTipo").val(_modeloVehiculo.tipo);
//    $("#txtCirculacion").val(_modeloVehiculo.circulacion);
//    $("#txtPlaca").val(_modeloVehiculo.placa);
//    $("#txtMarca").val(_modeloVehiculo.marca);
//    $("#txtModelo").val(_modeloVehiculo.modelo);
//    $("#txtMotor").val(_modeloVehiculo.motor);
//    $("#txtChasis").val(_modeloVehiculo.chasis); 
//    $("#txtUso").val(_modeloVehiculo.uso);
//    $("#txtAño").val(_modeloVehiculo.año);



//    $("#modalVehiculo").modal("show");

//}

//$(document).on("click", ".boton-nuevo-vehiculo", function () {

//    _modeloVehiculo.idCliente = 0,
//    _modeloVehiculo.tipo = "",
//    _modeloVehiculo.circulacion = "", 
//    _modeloVehiculo.placa = "",
//    _modeloVehiculo.marca = "",
//    _modeloVehiculo.modelo = "",
//    _modeloVehiculo.motor = "",
//    _modeloVehiculo.chasis = "",
//    _modeloVehiculo.uso = "",
//    _modeloVehiculo.año = "",

//    MostrarModal();

//})