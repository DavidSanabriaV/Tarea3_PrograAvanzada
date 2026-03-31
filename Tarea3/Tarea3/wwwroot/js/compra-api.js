const compraApi = {
    baseUrl: '/api/compras',

    crear: async function (compra) {
        const response = await fetch(this.baseUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include',
            body: JSON.stringify(compra)
        });
        const data = await response.json();
        if (!response.ok) return { success: false, status: response.status, data };
        return { success: true, data };
    },

    obtenerHistorial: async function (nombreCliente) {
        const response = await fetch(`${this.baseUrl}/${nombreCliente}`, {
            credentials: 'include'
        });
        if (!response.ok) throw new Error('Error al cargar historial');
        return await response.json();
    }
};