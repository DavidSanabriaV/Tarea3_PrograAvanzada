const eventoApi = {
    baseUrl: '/api/eventos',

    obtenerTodos: async function () {
        const response = await fetch(this.baseUrl, { credentials: 'include' });
        if (!response.ok) throw new Error('Error al obtener eventos');
        return await response.json();
    },

    obtenerPorId: async function (id) {
        const response = await fetch(`${this.baseUrl}/${id}`, { credentials: 'include' });
        if (response.status === 404) return null;
        if (!response.ok) throw new Error('Error al obtener evento');
        return await response.json();
    },

    crear: async function (evento) {
        const response = await fetch(this.baseUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify(evento)
        });
        const data = await response.json();
        if (!response.ok) return { success: false, status: response.status, data };
        return { success: true, data };
    }
};