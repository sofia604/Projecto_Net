@model IEnumerable<Projecto.Models.Pelicula>

@if (!Model.Any())
{
    < div class= "text-center text-muted" > Este actor no tiene películas registradas.</div>
}
else
{
    < table class= "table" >
        < thead >
            < tr >
                < th > Título </ th >
                < th > Fecha de Estreno</th>
            </tr>
        </thead>
        <tbody>
        @foreach (var pelicula in Model)
{
            < tr >
                < td > @pelicula.Titulo </ td >
                < td > @pelicula.FechaEstreno.ToString("d 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES")) </ td >
            </ tr >
        }
        </ tbody >
    </ table >
}
