// Modal functionality
document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById('paymentModal');
    const payButtons = document.querySelectorAll('.btn-primary'); // Botones de "Pagar"
    const closeButton = document.querySelector('.close-modal');
    const cancelButton = document.querySelector('.cancel-button');
    const monthCheckboxes = document.querySelectorAll('.month-checkbox');

    // Open modal when clicking pay button
    payButtons.forEach(button => {
        button.addEventListener('click', function (e) {
            // Obtener datos del alumno desde la fila de la tabla
            const row = button.closest('tr');
            const studentName = row.children[0].textContent; // Nombre completo
            const studentGrade = row.children[3].textContent; // Grado

            // Actualizar la información en el modal
            document.getElementById('studentName').textContent = studentName;
            document.getElementById('studentGrade').textContent = studentGrade;

            // Resetear selección de meses y valores del modal
            monthCheckboxes.forEach(checkbox => checkbox.checked = false);
            updateTotal();

            // Mostrar el modal
            modal.classList.add('active');
        });
    });

    // Close modal functions
    function closeModal() {
        modal.classList.remove('active');
        // Resetear el formulario del modal
        monthCheckboxes.forEach(checkbox => checkbox.checked = false);
        updateTotal();
    }

    closeButton.addEventListener('click', closeModal);
    cancelButton.addEventListener('click', closeModal);

    // Close modal when clicking outside
    modal.addEventListener('click', function (e) {
        if (e.target === modal) {
            closeModal();
        }
    });

    // Calculate total when selecting months
    function updateTotal() {
        let subtotal = 0;
        let mora = 0;

        monthCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                const amount = parseFloat(checkbox.dataset.amount);
                subtotal += amount;

                // Añadir mora para meses anteriores al mes actual (ejemplo: 10%)
                const monthNumber = parseInt(checkbox.id.replace('month', ''));
                const currentMonth = new Date().getMonth() + 1;
                if (monthNumber < currentMonth) {
                    mora += amount * 0.1;
                }
            }
        });

        document.getElementById('subtotal').textContent = `S/. ${subtotal.toFixed(2)}`;
        document.getElementById('mora').textContent = `S/. ${mora.toFixed(2)}`;
        document.getElementById('total').textContent = `S/. ${(subtotal + mora).toFixed(2)}`;
    }

    monthCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', updateTotal);
    });

    // Handle payment confirmation
    const confirmButton = document.querySelector('.confirm-button');
    confirmButton.addEventListener('click', function () {
        const selectedMethod = document.querySelector('input[name="paymentMethod"]:checked');
        if (!selectedMethod) {
            alert('Por favor seleccione un método de pago');
            return;
        }

        const selectedMonths = Array.from(monthCheckboxes)
            .filter(checkbox => checkbox.checked)
            .length;

        if (selectedMonths === 0) {
            alert('Por favor seleccione al menos un mes para pagar');
            return;
        }

        // Aquí puedes integrar la lógica de pago (API, servidor, etc.)
        alert('Procesando pago...');

        // Simular pago exitoso
        closeModal();
        updatePaymentStatus();
    });

    function updatePaymentStatus() {
        // Actualizar la interfaz para reflejar el estado del pago
        monthCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                const label = document.querySelector(`label[for="${checkbox.id}"]`);
                if (label) {
                    label.classList.add('paid'); // Clase CSS para indicar que está pagado
                }
            }
        });
    }
});
