document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('register-form');
    const alertContainer = document.getElementById('alert-container');

    form.addEventListener('submit', async function (e) {
        e.preventDefault();
        alertContainer.innerHTML = '';

        const usuario = {
            nombre: document.getElementById('Nombre').value.trim(),
            email: document.getElementById('Email').value.trim(),
            password: document.getElementById('Password').value,
            confirmPassword: document.getElementById('ConfirmPassword').value,
            cedula: document.getElementById('Cedula').value.trim(),
            edad: parseInt(document.getElementById('Edad').value)
        };

        try {
            const result = await authApi.register(usuario);

            if (result.success) {
                window.location.href = '/auth-frontend/login';
                return;
            }

            const errores = result.data.errors
                ? result.data.errors.join('<br>')
                : result.data.message || 'Error al registrarse.';
            alertContainer.innerHTML = '<div class="alert alert-danger">' + errores + '</div>';
        } catch (err) {
            alertContainer.innerHTML = '<div class="alert alert-danger">Error de conexión con el servidor.</div>';
            console.error(err);
        }
    });
});