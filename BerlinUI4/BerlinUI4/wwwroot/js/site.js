function getRandomBrightColor(existingColors) {
    let color;
    do {
        const r = Math.floor(Math.random() * 156) + 100; // Interval de la 100 la 255 pentru a evita culorile închise
        const g = Math.floor(Math.random() * 156) + 100;
        const b = Math.floor(Math.random() * 156) + 100;
        color = `rgb(${r}, ${g}, ${b})`;
    } while (existingColors.has(color));
    return color;
}

window.renderPieChart = (canvasId, data) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    const existingColors = new Set();
    const backgroundColors = data.data.map(() => {
        const color = getRandomBrightColor(existingColors);
        existingColors.add(color);
        return color;
    });

    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: data.labels,
            datasets: [{
                data: data.data,
                backgroundColor: backgroundColors,
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true,
                    position: 'right'
                },
                title: {
                    display: true,
                    text: data.title
                },
                datalabels: {
                    display: true,
                    formatter: (value, context) => {
                        return value + '%';
                    },
                    color: '#000',
                    anchor: 'end',
                    align: 'start',
                    offset: 10,
                    borderWidth: 1,
                    backgroundColor: (context) => {
                        return context.dataset.backgroundColor;
                    },
                    font: {
                        weight: 'bold',
                        size: 10
                    },
                    padding: 2
                }
            }
        },
        plugins: [ChartDataLabels]
    });
};

window.renderBarChart = (canvasId, data) => {
    const ctx = document.getElementById(canvasId).getContext('2d');
    const existingColors = new Set();
    const backgroundColors = data.data.map(() => {
        const color = getRandomBrightColor(existingColors);
        existingColors.add(color);
        return color;
    });

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: data.labels,
            datasets: [{
                data: data.data,
                backgroundColor: backgroundColors,
                borderColor: backgroundColors.map(color => {
                    // Generate a darker border color by reducing the RGB values
                    const rgb = color.match(/\d+/g).map(Number);
                    return `rgb(${rgb[0] - 50}, ${rgb[1] - 50}, ${rgb[2] - 50})`;
                }),
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: false // Ascunde legenda
                },
                title: {
                    display: true,
                    text: data.title
                }
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: data.xTitle // Setează eticheta pentru axa X
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: data.yTitle // Setează eticheta pentru axa Y
                    }
                }
            }
        }
    });
};

window.renderCustomBarChart = (canvasId, inputData, months) => {
    const ctx = document.getElementById(canvasId).getContext('2d');

    const existingColors = new Set();
    const backgroundColors = inputData.map(() => {
        const color = getRandomBrightColor(existingColors);
        existingColors.add(color);
        return color;
    });

    // Creăm dataset-uri pentru fiecare utilizator
    const datasets = inputData.map((userPerformance, index) => {
        const userMonthlyData = months.map(month => {
            const monthData = userPerformance.dateTotal.find(dateTotal => dateTotal.month.toLocaleString('default', { year: 'numeric', month: 'long' }) === month);
            return monthData ? monthData.totalAmount : 0;
        });
        return {
            label: userPerformance.user.name,
            data: userMonthlyData,
            backgroundColor: backgroundColors[index],
            borderColor: backgroundColors[index].replace(/\d+/g, (match, offset) => Math.max(0, match - 50)),
            borderWidth: 1
        };
    });

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: months,
            datasets: datasets
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true // Afișează legenda
                },
                title: {
                    display: true,
                    text: 'Sumele lunare ale utilizatorilor'
                }
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Luna' // Setează eticheta pentru axa X
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Suma' // Setează eticheta pentru axa Y
                    }
                }
            }
        }
    });
};

// Funcție pentru generarea unei culori luminoase random
function getRandomBrightColor(existingColors) {
    let color;
    do {
        color = `rgb(${Math.floor(Math.random() * 156 + 100)}, ${Math.floor(Math.random() * 156 + 100)}, ${Math.floor(Math.random() * 156 + 100)})`;
    } while (existingColors.has(color));
    return color;
}

window.renderTest = (idname, monts, usersList, usersValues) => {
    var ctx = document.getElementById(idname).getContext('2d');
    const existingColors = new Set();
    const backgroundColors = usersList.map(() => {
        const color = getRandomBrightColor(existingColors);
        existingColors.add(color);
        return color;
    });

    var list = []; // Inițializăm un array, nu un List
    for (var i = 0; i < usersList.length; i++) { // Inițializăm variabila `i`
        var userValues = usersList[i];

        console.log(userValues); 

        var item = {
            label: userValues, // Accesăm `label`, nu `title`
            data: usersValues[i],
            backgroundColor: backgroundColors[i],
        };
        list.push(item); // Folosim `push` pentru a adăuga elementul la array
    }

    var myPieChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: monts,
            datasets: list
        },
        options: {
            responsive: true,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            var label = context.dataset.label || '';
                            if (label) {
                                label += ': ';
                            }
                            if (context.parsed !== null) {
                                label += parseInt(context.dataset.data[context.dataIndex]) + " RON";
                            }
                            return label;
                        }
                    }
                }
            }
        }
    });
}
