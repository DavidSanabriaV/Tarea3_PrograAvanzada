const authApi = {
    baseUrl: '/api/auth',

    login: async function (credenciales) {
        const response = await fetch(this.baseUrl + '/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify(credenciales)
        });
        const data = await response.json();
        if (!response.ok) return { success: false, data };
        return { success: true, data };
    },

    register: async function (usuario) {
        const response = await fetch(this.baseUrl + '/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify(usuario)
        });
        const data = await response.json();
        if (!response.ok) return { success: false, data };
        return { success: true, data };
    },

    logout: async function () {
        await fetch(this.baseUrl + '/logout', {
            method: 'POST',
            credentials: 'include'
        });
        window.location.href = "/";
    }
};