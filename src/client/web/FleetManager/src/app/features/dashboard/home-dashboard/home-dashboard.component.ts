import {Component, OnInit} from '@angular/core';
import {HomeDashboardService} from './service/home-dashboard.service';
import {HomeDashboard} from './models/home-dashboard';
import {VehicleWithUpcomingMaintenance} from './models/vehicle-with-upcoming-maintenance';

@Component({
  selector: 'app-home-dashboard',
  standalone: false,

  templateUrl: './home-dashboard.component.html',
  styleUrl: './home-dashboard.component.css'
})
export class HomeDashboardComponent implements OnInit {
  public vehicleCountByStatusChartOptions: any;
  public routeCountByStatusChartOptions: any;
  public vehiclesWithUpcomingMaintenanceDataSource: VehicleWithUpcomingMaintenance[] = [];
  public displayedColumns: string[] = ['vin', 'licensePlate', 'model', 'nextInspectionDate'];

  constructor(private homeDashboardService: HomeDashboardService) {

  }

  ngOnInit(): void {
        this.homeDashboardService.getHomeDashboard().subscribe({
          next: (homeDashboard: HomeDashboard) => {
            this.handleHomeDashboard(homeDashboard);
          },
          error: (error: Error) => {console.log(error)}
        })
    }

  private handleHomeDashboard(homeDashboard: HomeDashboard): void {
    this.vehiclesWithUpcomingMaintenanceDataSource = homeDashboard.vehiclesWithUpcomingMaintenance;
    this.updateVehicleCountByStatusChart(homeDashboard.vehiclesCountPerStatus);
    this.updateRouteCountByStatusChart(homeDashboard.routesCountPerStatus);
  }

  private updateVehicleCountByStatusChart(vehicleCountPerStatus:  { [p: string]: number }){
    const chartData = Object.entries(vehicleCountPerStatus).map(([key, value]) => ({
      name: key,
      value,
    }));

    this.vehicleCountByStatusChartOptions = {
      title: {
        text: 'Vehicle Count by Status',
        left: 'center',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        left: 'left',
      },
      series: [
        {
          name: 'Vehicles',
          type: 'pie',
          radius: '50%',
          data: chartData,
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
          },
          animationType: 'expand',
          animationEasing: 'elasticOut',
          animationDuration: 1000,
        },
      ],
    };
  }

  private updateRouteCountByStatusChart(routeCountPerStatus:  { [p: string]: number }){
    const chartData = Object.entries(routeCountPerStatus).map(([key, value]) => ({
      name: key,
      value,
    }));

    this.routeCountByStatusChartOptions = {
      title: {
        text: 'Route Count by Status',
        left: 'center',
      },
      tooltip: {
        trigger: 'item',
      },
      legend: {
        orient: 'vertical',
        left: 'left',
      },
      series: [
        {
          name: 'Vehicles',
          type: 'pie',
          radius: '50%',
          data: chartData,
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)',
            },
          },
        },
      ],
    };
  }
}
