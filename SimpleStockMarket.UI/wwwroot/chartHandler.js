// Store chart instances globally
window.chartInstances = {};

function createStockChart(canvasId) {
    var ctx = document.getElementById(canvasId).getContext('2d');

    // Destroy existing chart instance for the canvas if it exists
    if (window.chartInstances[canvasId]) {
        window.chartInstances[canvasId].destroy();
    }

    // Create and store the new chart instance
    window.chartInstances[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
            datasets: [{
                label: 'Stock Data',
                data: Array.from({ length: 6 }, () => Math.floor(Math.random() * 100)), // Sample Data
                borderColor: 'green',
                borderWidth: 2,
                fill: false
            }]
        },
        options: {
            responsive: false,
            maintainAspectRatio: false
        }
    });
}

