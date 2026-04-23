import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './weather.html',
  styleUrls: ['./weather.css']
})
export class WeatherComponent {

  @Input() weatherData: any[] = [];

  searchText: string = '';
  filteredList: any[] = [];

  ngOnInit() {
    this.filteredList = this.weatherData;
  }

  onSearch() {
    const city = this.searchText.trim().toLowerCase();

    if (!city) {
      this.filteredList = this.weatherData;
      return;
    }

    this.filteredList = this.weatherData.filter(item =>
      item.name.toLowerCase().includes(city)
    );
  }
}