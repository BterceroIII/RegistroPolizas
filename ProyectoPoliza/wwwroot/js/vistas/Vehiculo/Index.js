const _modeloVehiculo = {
    idVehiculo: 0,
    idCliente: 0,
    tipo: "",
    noCirculacion: "",
    noPlaca: "",
    marca: "",
    modelo: "",
    noMotor: "",
    noChasis: "",
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
                            $("<td>").text(vehiculo.refCliente.nombreCliente),
                            $("<td>").text(vehiculo.tipo),
                            $("<td>").text(vehiculo.modelo),
                            $("<td>").text(vehiculo.noPlaca),

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

    fetch("/Clientes/ListaClientes")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {

            if (responseJson.length > 0) {
                responseJson.forEach((item) => {

                    $("#cboNombre").append(
                        $("<option>").val(item.idCliente).text(item.nombreCliente)
                    )

                })
            }

        })



}, false)



function MostrarModal() {
    $("#cboNombre").val(_modeloVehiculo.idCliente == 0 ? $("#cboNombre option:first").val() : _modeloVehiculo.idCliente)
   /* $("#cboNombre").val(_modeloVehiculo.idCliente == 0 ? $("#cboNombre").text() : _modeloVehiculo.idCliente);*/
    $("#cboTipo").val(_modeloVehiculo.tipo);
    $("#txtCirculacion").val(_modeloVehiculo.noCirculacion);
    $("#txtPlaca").val(_modeloVehiculo.noPlaca);
    $("#txtMarca").val(_modeloVehiculo.marca);
    $("#txtModelo").val(_modeloVehiculo.modelo);
    $("#txtMotor").val(_modeloVehiculo.noMotor);
    $("#txtChasis").val(_modeloVehiculo.noChasis);
    $("#txtUso").val(_modeloVehiculo.uso);
    $("#txtAño").val(_modeloVehiculo.año);



    $("#modalVehiculo").modal("show");

}

$(document).on("click", ".boton-nuevo-vehiculo", function () {

        _modeloVehiculo.idCliente = 0,
        _modeloVehiculo.tipo = "",
        _modeloVehiculo.noCirculacion = "",
        _modeloVehiculo.noPlaca = "",
        _modeloVehiculo.marca = "",
        _modeloVehiculo.modelo = "",
        _modeloVehiculo.noMotor = "",
        _modeloVehiculo.noChasis = "",
        _modeloVehiculo.uso = "",
        _modeloVehiculo.año = "",

        MostrarModal();

})

$(document).on("click", ".boton-editar-vehiculo", function () {

    const _vehiculo = $(this).data("dataVehiculo");


    _modeloVehiculo.idVehiculo = _vehiculo.idVehiculo;
    _modeloVehiculo.idCliente = _vehiculo.refCliente.idCliente;
    _modeloVehiculo.tipo = _vehiculo.tipo;
    _modeloVehiculo.noCirculacion = _vehiculo.noCirculacion;
    _modeloVehiculo.noPlaca = _vehiculo.noPlaca;
    _modeloVehiculo.marca = _vehiculo.marca;
    _modeloVehiculo.modelo = _vehiculo.modelo;
    _modeloVehiculo.noMotor = _vehiculo.noMotor;
    _modeloVehiculo.noChasis = _vehiculo.noChasis;
    _modeloVehiculo.uso = _vehiculo.uso;
    _modeloVehiculo.año = _vehiculo.año;


    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-vehiculo", function () {

    const modelo = {

        idVehiculo: _modeloVehiculo.idVehiculo,
        refCliente: {
            idCliente: $("#cboNombre").val()
        },
        tipo: $("#cboTipo").val(),
        noPlaca: $("#txtPlaca").val(),
        marca: $("#txtMarca").val(),
        modelo: $("#txtModelo").val(),
        noMotor: $("#txtMotor").val(),
        noChasis: $("#txtChasis").val(),
        uso: $("#txtUso").val(),
        año: $("#txtAño").val(),
    }


    if (_modeloVehiculo.idVehiculo == 0) {

        fetch("/Circulaciones/GuardarVehiculo", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalVehiculo").modal("hide");
                    Swal.fire("Listo!", "Vehiculo fue creado", "success");
                    MostrarVehiculo();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Circulaciones/EditarVehiculo", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalVehiculo").modal("hide");
                    Swal.fire("Listo!", "Vehiculo fue actualizado", "success");
                    MostrarVehiculo();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})

$(document).on("click", ".boton-eliminar-vehiculo", function () {

    const _vehiculo = $(this).data("dataVehiculo");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Vehiculo "${_vehiculo.noPlaca}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Circulaciones/EliminarVehiculo?idVehiculo=${_vehiculo.idVehiculo}`, {
                method: "PUT"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Vehiculo fue elminado", "success");
                        MostrarVehiculo();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})