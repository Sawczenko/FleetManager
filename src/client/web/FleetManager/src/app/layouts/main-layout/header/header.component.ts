import {Component, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  @Output() menuButtonClickedEvent = new EventEmitter();

  public menuButtonClick(){
    this.menuButtonClickedEvent.emit();
  }
}