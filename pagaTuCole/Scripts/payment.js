// Modal functionality
document.addEventListener('DOMContentLoaded', function() {
    const modal = document.getElementById('paymentModal');
    const payButtons = document.querySelectorAll('.pay-button');
    const closeButton = document.querySelector('.close-modal');
    const cancelButton = document.querySelector('.cancel-button');
    const monthCheckboxes = document.querySelectorAll('.month-checkbox');
    
    // Open modal when clicking pay button
    payButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            const studentCard = e.target.closest('.student-card');
            const studentName = studentCard.querySelector('h4').textContent;
            const studentGrade = studentCard.querySelector('p').textContent;
            
            document.getElementById('studentName').textContent = studentName;
            document.getElementById('studentGrade').textContent = studentGrade;
            
            modal.classList.add('active');
        });
    });
    
    // Close modal functions
    function closeModal() {
        modal.classList.remove('active');
        // Reset form
        monthCheckboxes.forEach(checkbox => checkbox.checked = false);
        updateTotal();
    }
    
    closeButton.addEventListener('click', closeModal);
    cancelButton.addEventListener('click', closeModal);
    
    // Close modal when clicking outside
    modal.addEventListener('click', function(e) {
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
                
                // Add mora for previous months (example: 10% for months before current)
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
    confirmButton.addEventListener('click', function() {
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
        
        // Here you would typically integrate with a payment gateway
        alert('Procesando pago...');
        // After successful payment:
        closeModal();
        // Update the UI to reflect the payment
        updatePaymentStatus();
    });
    
    function updatePaymentStatus() {
        // Update the months grid to show paid months
        // This is a simplified example - in a real application, 
        // you would update based on the server response
        monthCheckboxes.forEach(checkbox => {
            if (checkbox.checked) {
                const monthNumber = parseInt(checkbox.id.replace('month', ''));
                const monthsGrid = document.querySelectorAll('.months-grid');
                monthsGrid.forEach(grid => {
                    const monthElement = grid.children[monthNumber - 1];
                    if (monthElement) {
                        monthElement.classList.remove('pending');
                        monthElement.classList.add('paid');
                    }
                });
            }
        });
    }
});