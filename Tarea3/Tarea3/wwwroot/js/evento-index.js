document.addEventListener('DOMContentLoaded', cargarEventos);

async function cargarEventos() {
    const tbody = document.getElementById('eventos-tbody');
    const loading = document.getElementById('eventos-loading');

    try {
        const eventos = await eventoApi.obtenerTodos();
        loading.classList.add('d-none');

        if (eventos.length === 0) {
            tbody.innerHTML = `<tr><td colspan="6" class="text-center text-muted">No hay eventos registrados.</td></tr>`;
            return;
        }

        tbody.innerHTML = eventos.map(e => `
            <tr>
                <td>${e.nombre}</td>
                <td>${new Date(e.fecha).toLocaleDateString()}</td>
                <td>${e.lugar}</td>
                <td>₡${e.precio.toFixed(2)}</td>
                <td>${e.cantidadDisponible > 0
                ? `<span class="badge bg-success">${e.cantidadDisponible} disponibles</span>`
                : `<span class="badge bg-danger">Agotado</span>`}
                </td>
                <td>
                    ${e.cantidadDisponible > 0
                ? `<button class="btn btn-primary btn-sm btn-comprar" data-id="${e.id}">Comprar</button>`
                : ''}
                </td>
            </tr>`).join('');

        document.querySelectorAll('.btn-comprar').forEach(btn => {
            btn.addEventListener('click', function () {
                window.location.href = `/compras/comprar/${this.dataset.id}`;
            });
        });

    } catch (error) {
        loading.classList.add('d-none');
        tbody.innerHTML = `<tr><td colspan="6" class="text-center text-danger">Error al cargar eventos.</td></tr>`;
    }
}