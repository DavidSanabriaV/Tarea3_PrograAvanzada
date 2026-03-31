document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('login-form');
    const alertContainer = document.getElementById('alert-container');

    form.addEventListener('submit', async function (e) {
        e.preventDefault();
        alertContainer.innerHTML = '';

        const credenciales = {
            email: document.getElementById('Email').value.trim(),
            password: document.getElementById('Password').value,
            rememberMe: false
        };

        const result = await authApi.login(credenciales);

        if (result.success) {
            window.location.href = '/eventos';
            return;
        }

        alertContainer.innerHTML = `
            <div class="alert alert-danger">${result.data.message || 'Error al iniciar sesión.'}</div>`;
    });
});