const _modeloEmpleado = {
    idEmpleado: 0,
    nombre: "",
    cedula: "",
    telefono: "",
    cargo: "",
/*    eliminado: 0*/
}

function MostrarEmpleados() {

    fetch("/Empleados/ListaEmpleados")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {

                $("#tablaEmpleados tbody").html("");


                responseJson.forEach((empleado) => {
                    
                    $("#tablaEmpleados tbody").append(
                        $("<tr>").append(
                            $("<td>").text(empleado.nombre),
                            $("<td>").text(empleado.cedula),
                            $("<td>").text(empleado.telefono),
                            $("<td>").text(empleado.cargo),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-empleado").text("Editar").data("dataEmpleado", empleado),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-empleado").text("Eliminar").data("dataEmpleado", empleado)
                            )
                        )
                    )
                })

            }


        })


}

//function MostrarEmpleados() {

//    fetch("/Empleados/ListaEmpleados")
//        .then((response) => {
//            return response.ok ? response.json() : Promise.reject(response)
//        })
//        .then((dataJson) => {

//            $("#tablaEmpleados tbody").html("");

//            dataJson.forEach((item) => {

//                $("#tablaEmpleados tbody").append(
//                    $("<tr>").append(
//                        $("<td>").text(item.Nombre),
//                        $("<td>").text(item.Cedula),
//                        $("<td>").text(item.Telefono),
//                        $("<td>").text(item.Cargo),
//                        $("<td>").text(item.Eliminado),
//                        $("<td>").append(
//                            $("<button>").addClass("btn btn-primary btn-sm boton-editar-empleado").text("Editar").data("dataEmpleado", Empleado),
//                            $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-empleado").text("Eliminar").data("dataEmpleado", Empleado)
//                        )
//                    )
//                )

//            })


//        })

//}

document.addEventListener("DOMContentLoaded", function () {
    MostrarEmpleados();


}, false)


function MostrarModal() {

    $("#txtNombre").val(_modeloEmpleado.nombre);
    $("#txtCedula").val(_modeloEmpleado.cedula);
    $("#txtTelefono").val(_modeloEmpleado.telefono);
    $("#cboCargo").val(_modeloEmpleado.cargo);



    $("#modalEmpleado").modal("show");

}

$(document).on("click", ".boton-nuevo-empleado", function () {

    _modeloEmpleado.idEmpleado = 0;
    _modeloEmpleado.nombre = "";
    _modeloEmpleado.cedula = "";
    _modeloEmpleado.telefono = "";
    _modeloEmpleado.cargo = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");


    _modeloEmpleado.idEmpleado = _empleado.idEmpleado;
    _modeloEmpleado.nombre = _empleado.nombre;
    _modeloEmpleado.cedula = _empleado.cedula;
    _modeloEmpleado.telefono = _empleado.telefono;
    _modeloEmpleado.cargo = _empleado.cargo;
   

    MostrarModal();

})

$(document).on("click", ".boton-guardar-cambios-empleado", function () {

    const modelo = {
        idEmpleado: _modeloEmpleado.idEmpleado,
        nombre: $("#txtNombre").val(),
        cedula: $("#txtCedula").val(),
        telefono: $("#txtTelefono").val(),
        cargo: $("#cboCargo").val(),
    }


    if (_modeloEmpleado.idEmpleado == 0) {

        fetch("/Empleados/GuardarEmpleado", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue creado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo crear", "error");
            })

    } else {

        fetch("/Empleados/EditarEmpleado", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue actualizado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No se puedo actualizar", "error");
            })

    }


})

$(document).on("click", ".boton-eliminar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar empleado "${_empleado.nombre}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Empleados/EliminarEmpleado?idEmpleado=${_empleado.idEmpleado}`, {
                method: "PUT"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarEmpleados();
                    }
                    else
                        Swal.fire("Lo sentimos", "No se puedo eliminar", "error");
                })

        }



    })

})