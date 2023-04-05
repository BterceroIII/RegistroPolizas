const _modeloPoliza = {
    idPoliza: 0,
    aseguradora: "",
    tipo: "",
    codigo: ""
};

function MostrarPoliza() {

    fetch("/Polizas/ListaPoliza")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaPoliza tbody").html("");


                responseJson.forEach((poliza) => {

                    $("#tablaPoliza tbody").append(
                        $("<tr>").append(
                            $("<td>").text(poliza.idPoliza),
                            $("<td>").text(poliza.aseguradora),
                            $("<td>").text(poliza.tipo),
                            $("<td>").text(poliza.codigo),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-poliza").text("Editar").data("dataPoliza", poliza),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-poliza").text("Eliminar").data("dataPoliza", poliza)
                            )
                        )
                    )
                })

            }


        })


}

document.addEventListener("DOMContentLoaded", function () {
    MostrarPoliza();


}, false)


function MostrarModal() {

    $("#cboAseguradora").val(_modeloPoliza.aseguradora);
    $("#cboTipo").val(_modeloPoliza.tipo);
    $("#txtCodigo").val(_modeloPoliza.codigo);




    $("#modalPoliza").modal("show");

}

$(document).on("click", ".boton-nuevo-poliza", function () {

    _modeloPoliza.idPoliza = 0;
    _modeloPoliza.aseguradora = "";
    _modeloPoliza.tipo = "";
    _modeloPoliza.codigo = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-poliza", function () {

    const _poliza = $(this).data("dataPoliza");


    _modeloPoliza.idPoliza = _poliza.idEmpleado;
    _modeloPoliza.aseguradora = _poliza.aseguradora;
    _modeloPoliza.tipo = _poliza.tipo;
    _modeloPoliza.codigo = _poliza.codigo;


    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-poliza", function () {

    const modelo = {
        idPoliza: _modeloPoliza.idPoliza,
        aseguradora: $("#cboAseguradora").val(),
        tipo: $("#cboTipo").val(),
        codigo: $("#txtCodigo").val(),
    }


    if (_modeloPoliza.idPoliza == 0) {

        fetch("/Polizas/GuardarPoliza", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalPoliza").modal("hide");
                    Swal.fire("Listo!", "Poliza fue creado", "success");
                    MostrarPoliza();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Polizas/EditarPoliza", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalPoliza").modal("hide");
                    Swal.fire("Listo!", "Poliza fue actualizado", "success");
                    MostrarPoliza();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})

$(document).on("click", ".boton-eliminar-poliza", function () {

    const _poliza = $(this).data("dataPoliza");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar poliza Numero"${_poliza.codigo}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Polizas/EliminarPoliza?idPoliza=${_poliza.idPoliza}`, {
                method: "PUT"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Poliza fue elminado", "success");
                        MostrarPoliza();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })
        }
    })
})