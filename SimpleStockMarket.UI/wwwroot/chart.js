window.renderStockChart = (labels, prices) => {
    var ctx = document.getElementById('stockChart').getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Stock Price',
                data: prices,
                borderColor: 'blue',
                fill: false
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: { enabled: false },
            },
            scales: {
                x: { display: false },
                y: {
                    display: true,
                    position: "right",
                    grid: { display: false },
                    ticks: {
                        color: "rgb(156, 163, 175)", // gray-400 in Tailwind
                        font: { size: 10 },
                        callback: function (value, index, values) {
                            // Only show the top and bottom values
                            if (index === 0 || index === values.length - 1) {
                                return "$" + value.toFixed(2);
                            }
                            return "";
                        },
                        count: 2, // This ensures only 2 ticks are shown
                    },
                },
            },
            animation: false,
        },
    });
};
