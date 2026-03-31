document.addEventListener('DOMContentLoaded', async function () {
    const container = document.getElementById('compra-container');
    const eventoId = parseInt(container.dataset.eventoId);
    const alertContainer = document.getElementById('alert-container');

    try {
        const evento = await eventoApi.obtenerPorId(eventoId);
        if (!evento) {
            container.innerHTML = <div class="alert alert-warning">Evento no encontrado.</div>;
            return;
        }

        document.getElementById('evento-nombre').textContent = evento.nombre;
        document.getElementById('evento-lugar').textContent = evento.lugar;
        document.getElementById('evento-fecha').textContent = new Date(evento.fecha).toLocaleDateString();
        document.getElementById('evento-precio').textContent = ₡${ evento.precio.toFixed(2) };
        document.getElementById('precio-unitario').dataset.precio = evento.precio;

    } catch {
        container.innerHTML = <div class="alert alert-danger">Error al cargar el evento.</div>;
        return;
    }

    document.getElementById('Cantidad').addEventListener('input', function () {
        const precio = parseFloat(document.getElementById('precio-unitario').dataset.precio) || 0;
        const cantidad = parseInt(this.value) || 0;
        document.getElementById('total-display').textContent = ₡${ (precio * cantidad).toFixed(2) };
    });

    document.getElementById('compra-form').addEventListener('submit', async function (e) {
        e.preventDefault();
        alertContainer.innerHTML = '';

        const compra = {
            eventoId: eventoId,
            nombreCliente: document.getElementById('NombreCliente').value.trim(),
            cantidad: parseInt(document.getElementById('Cantidad').value) || 0
        };

        const result = await compraApi.crear(compra);

        if (result.success) {
            alertContainer.innerHTML = `
                <div class="alert alert-success">
                    ✅ Compra realizada. Total: ₡${result.data.total?.toFixed(2)}
                </div>`;
            setTimeout(() => {
                const nombre = document.getElementById('NombreCliente').value.trim();
                window.location.href = /compras/historial / ${ nombre };
            }, 2000);
            return;
        }

        alertContainer.innerHTML = `
            <div class="alert alert-danger">${result.data.message || 'Error al procesar la compra.'}</div>`;
    });
});