document.addEventListener('DOMContentLoaded', cargarHistorial);

async function cargarHistorial() {
    const container = document.getElementById('historial-container');
    let nombreCliente = container.dataset.cliente;
    if (!nombreCliente) {
        nombreCliente = localStorage.getItem("clienteCompra");
    }
    const loading = document.getElementById('historial-loading');
    const tbody = document.getElementById('historial-tbody');

    try {
        const compras = await compraApi.obtenerHistorial(nombreCliente);
        loading.classList.add('d-none');

        if (compras.length === 0) {
            tbody.innerHTML = `<tr><td colspan="5" class="text-center text-muted">No hay compras registradas.</td></tr>`;
            return;
        }

        tbody.innerHTML = compras.map(c => `
            <tr>
                <td>${c.evento ? c.evento.nombre : 'N/A'}</td>
                <td>${c.nombreCliente}</td>
                <td>${c.cantidad}</td>
                <td>₡${c.total.toFixed(2)}</td>
                <td>${new Date(c.fechaCompra).toLocaleDateString()}</td>
            </tr>`).join('');

    } catch {
        loading.classList.add('d-none');
        tbody.innerHTML = `<tr><td colspan="5" class="text-center text-danger">Error al cargar el historial.</td></tr>`;
    }
}