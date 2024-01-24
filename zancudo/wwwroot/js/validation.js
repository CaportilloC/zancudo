function validateDates() {
    var startDate = document.getElementById('FechaInicio').value;
    var finishDate = document.getElementById('FechaFin').value;
    var dateError = document.getElementById('dateError');

    var maxDaysDifference = 60;

    var startDateObj = new Date(startDate);
    var finishDateObj = new Date(finishDate);

    var daysDifference = (finishDateObj - startDateObj) / (1000 * 60 * 60 * 24);

    if (startDateObj >= finishDateObj || daysDifference > maxDaysDifference) {
        dateError.textContent = 'La fecha de inicio debe ser estrictamente menor a la fecha final y la diferencia entre las fechas no debe ser mayor a 60 días';
    } else {
        dateError.textContent = '';
    }
}