const authApi = {
    baseUrl: '/api/auth',

    login: async function (credenciales) {
        try {
            const response = await fetch(this.baseUrl + '/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
                body: JSON.stringify(credenciales)
            });

            const data = await response.json();

            if (!response.ok) return { success: false, data };
            return { success: true, data };
        } catch (error) {
            return {
                success: false,
                data: { message: 'No se pudo conectar con el servidor.' }
            };
        }
    },

    register: async function (usuario) {
        try {
            const response = await fetch(this.baseUrl + '/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include',
                body: JSON.stringify(usuario)
            });

            const data = await response.json();

            if (!response.ok) return { success: false, data };
            return { success: true, data };
        } catch (error) {
            return {
                success: false,
                data: { message: 'No se pudo conectar con el servidor.' }
            };
        }
    },

    logout: async function () {
        try {
            const response = await fetch(this.baseUrl + '/logout', {
                method: 'POST',
                credentials: 'include'
            });

            if (response.ok) {
                window.location.href = '/';
            } else {
                alert('No se pudo cerrar la sesión.');
            }
        } catch (error) {
            console.error('Error logout:', error);
            alert('Error al cerrar sesión.');
        }
    },

    initLogout: function () {
        const logoutLink = document.getElementById('logout-link');

        if (logoutLink) {
            logoutLink.addEventListener('click', async function (e) {
                e.preventDefault();
                await authApi.logout();
            });
        }
    }
};

document.addEventListener('DOMContentLoaded', function () {
    authApi.initLogout();
});