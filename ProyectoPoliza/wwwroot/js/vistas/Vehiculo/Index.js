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





function MostrarModal() {

    $("#txtNombre").val(_modeloVehiculo.idCliente);
    $("#cboTipo").val(_modeloVehiculo.tipo);
    $("#txtCirculacion").val(_modeloVehiculo.circulacion);
    $("#txtPlaca").val(_modeloVehiculo.placa);
    $("#txtMarca").val(_modeloVehiculo.marca);
    $("#txtModelo").val(_modeloVehiculo.modelo);
    $("#txtMotor").val(_modeloVehiculo.motor);
    $("#txtChasis").val(_modeloVehiculo.chasis); 
    $("#txtUso").val(_modeloVehiculo.uso);
    $("#txtAño").val(_modeloVehiculo.año);



    $("#modalVehiculo").modal("show");

}

$(document).on("click", ".boton-nuevo-vehiculo", function () {

    _modeloVehiculo.idCliente = 0,
    _modeloVehiculo.tipo = "",
    _modeloVehiculo.circulacion = "", 
    _modeloVehiculo.placa = "",
    _modeloVehiculo.marca = "",
    _modeloVehiculo.modelo = "",
    _modeloVehiculo.motor = "",
    _modeloVehiculo.chasis = "",
    _modeloVehiculo.uso = "",
    _modeloVehiculo.año = "",

    MostrarModal();

})