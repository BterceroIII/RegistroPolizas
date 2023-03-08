const _modeloCliente = {
    idCliente: 0,
    nombreCliente: "",
    tipoDocIdentidad: "",
    noDocIdentidad: "",
    nacionalidad: "",
    telefono: "",
    direccion: "",
    correo: "",
    /*    eliminado: 0*/
}

function MostrarCliente() {

    fetch("/Clientes/ListaClientes")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaCliente tbody").html("");


                responseJson.forEach((cliente) => {

                    $("#tablaCliente tbody").append(
                        $("<tr>").append(
                            $("<td>").text(cliente.idCliente),
                            $("<td>").text(cliente.nombreCliente),
                            $("<td>").text(cliente.tipoDocIdentidad),
                            $("<td>").text(cliente.noDocIdentidad),
                            $("<td>").text(cliente.telefono),

                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-cliente").text("Editar").data("dataCliente", cliente),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-cliente").text("Eliminar").data("dataCliente", cliente)
                            )
                        )
                    )
                })

            }


        })


}

document.addEventListener("DOMContentLoaded", function () {
    MostrarCliente();


}, false)


function MostrarModal() {

    $("#txtNombre").val(_modeloCliente.nombreCliente);
    $("#cboTipo").val(_modeloCliente.tipoDocIdentidad);
    $("#txtIdentidad").val(_modeloCliente.noDocIdentidad);
    $("#txtNacionalidad").val(_modeloCliente.nacionalidad);
    $("#txtCorreo").val(_modeloCliente.correo);
    $("#txtDireccion").val(_modeloCliente.direccion);
    $("#txtTelefono").val(_modeloCliente.telefono);



    $("#modalCliente").modal("show");

}

$(document).on("click", ".boton-nuevo-cliente", function () {

    _modeloCliente.idCliente = 0;
    _modeloCliente.nombreCliente = "";
    _modeloCliente.tipoDocIdentidad = "";
    _modeloCliente.noDocIdentidad = "";
    _modeloCliente.nacionalidad = "";
    _modeloCliente.correo = "";
    _modeloCliente.direccion = "";
    _modeloCliente.telefono = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-cliente", function () {

    const _cliente = $(this).data("dataCliente");


    _modeloCliente.idCliente = _cliente.idCliente;
    _modeloCliente.nombreCliente = _cliente.nombreCliente;
    _modeloCliente.tipoDocIdentidad = _cliente.tipoDocIdentidad;
    _modeloCliente.noDocIdentidad = _cliente.noDocIdentidad;
    _modeloCliente.nacionalidad = _cliente.nacionalidad;
    _modeloCliente.correo = _cliente.correo;
    _modeloCliente.direccion = _cliente.direccion;
    _modeloCliente.telefono = _cliente.telefono;


    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-cliente", function () {

    const modelo = {
        idCliente: _modeloCliente.idCliente,
        nombreCliente: $("#txtNombre").val(),
        tipoDocIdentidad: $("#cboTipo").val(),
        noDocIdentidad: $("#txtIdentidad").val(),
        nacionalidad: $("#txtNacionalidad").val(),
        telefono: $("#txtTelefono").val(),
        correo: $("#txtCorreo").val(),
        direccion: $("#txtDireccion").val(),
        /*    eliminado: 0*/
    }


    if (_modeloCliente.idCliente == 0) {

        fetch("/Clientes/GuardarCliente", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalCliente").modal("hide");
                    Swal.fire("Listo!", "Cliente fue creado", "success");
                    MostrarCliente();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Clientes/EditarCliente", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalCliente").modal("hide");
                    Swal.fire("Listo!", "Cliente fue actualizado", "success");
                    MostrarCliente();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})


$(document).on("click", ".boton-eliminar-cliente", function () {

    const _cliente = $(this).data("dataCliente");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar Cliente "${_cliente.nombreCliente}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Clientes/EliminarCliente?idCliente=${_cliente.idCliente}`, {
                method: "PUT"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Clietne fue elminado", "success");
                        MostrarCliente();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})