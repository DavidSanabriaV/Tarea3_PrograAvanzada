document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('crear-evento-form');
    const alertContainer = document.getElementById('alert-container');

    form.addEventListener('submit', async function (e) {
        e.preventDefault();
        limpiarErrores();

        const evento = {
            nombre: document.getElementById('Nombre').value.trim(),
            fecha: document.getElementById('Fecha').value,
            lugar: document.getElementById('Lugar').value.trim(),
            precio: parseFloat(document.getElementById('Precio').value) || 0,
            cantidadDisponible: parseInt(document.getElementById('CantidadDisponible').value) || 0
        };

        const result = await eventoApi.crear(evento);

        if (result.success) {
            window.location.href = '/eventos';
            return;
        }

        if (result.status === 400 && result.data.errors) {
            result.data.errors.forEach(err => mostrarAlerta(err));
        } else {
            mostrarAlerta(result.data.message || 'Error inesperado.');
        }
    });

    function mostrarAlerta(mensaje) {
        alertContainer.innerHTML = `
            <div class="alert alert-danger alert-dismissible fade show">
                ${mensaje}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>`;
    }

    function limpiarErrores() {
        alertContainer.innerHTML = '';
    }
});