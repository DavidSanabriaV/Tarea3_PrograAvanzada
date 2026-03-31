document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('register-form');
    const alertContainer = document.getElementById('alert-container');

    form.addEventListener('submit', async function (e) {
        e.preventDefault();
        alertContainer.innerHTML = '';

        const usuario = {
            email: document.getElementById('Email').value.trim(),
            password: document.getElementById('Password').value,
            nombre: document.getElementById('Nombre').value.trim()
        };

        const result = await authApi.register(usuario);

        if (result.success) {
            window.location.href = '/auth/login';
            return;
        }

        const errores = result.data.errors
            ? result.data.errors.join('<br>')
            : result.data.message || 'Error al registrarse.';

        alertContainer.innerHTML = `<div class="alert alert-danger">${errores}</div>`;
    });
});