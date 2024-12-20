import { Component } from '@angular/core';

@Component({
  selector: 'app-home-dashboard',
  standalone: false,

  templateUrl: './home-dashboard.component.html',
  styleUrl: './home-dashboard.component.css'
})
export class HomeDashboardComponent {
  public options: any

  constructor() {
    this.options = {
      title: {
        text: 'Sales by Category',
        left: 'center'
      },
      tooltip: {
        trigger: 'item'
      },
      legend: {
        orient: 'vertical',
        left: 'left'
      },
      series: [
        {
          name: 'Sales',
          type: 'pie',
          radius: '50%',
          data: [
            { value: 1048, name: 'Electronics' },
            { value: 735, name: 'Clothing' },
            { value: 580, name: 'Home Appliances' },
            { value: 484, name: 'Books' },
            { value: 300, name: 'Other' }
          ],
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
          }
        }
      ]
    };
  }

}
