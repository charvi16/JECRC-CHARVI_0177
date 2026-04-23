import { Component } from '@angular/core';
import { WeatherComponent } from './weather/weather';


@Component({
  standalone : true,
  imports: [WeatherComponent],
  selector: 'app-root',
  templateUrl: './app.html'
})
export class AppComponent {

  weatherRecords = [
    {
      name: 'London',
      temperature: '10°C',
      wind: '12 km/h',
      humidity: '80%'
    },
    {
      name: 'Paris',
      temperature: '14°C',
      wind: '10 km/h',
      humidity: '70%'
    },
    {
      name: 'New York',
      temperature: '8°C',
      wind: '15 km/h',
      humidity: '60%'
    }
  ];
}