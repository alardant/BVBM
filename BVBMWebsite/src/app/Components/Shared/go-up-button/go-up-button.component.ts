import { ViewportScroller } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-go-up-button',
  templateUrl: './go-up-button.component.html',
  styleUrls: ['./go-up-button.component.css']
})
export class GoUpButtonComponent implements OnInit {
  showButton: boolean = false;

  constructor(private viewportScroller: ViewportScroller) { }

  ngOnInit() {
  }

  @HostListener('window:scroll', ['$event'])
  onScroll(event: Event) {
    // Vérifiez la position de défilement et définissez showButton en conséquence.
    this.showButton = window.pageYOffset > 0;
  }

  GoBackUp() {
    this.viewportScroller.scrollToPosition([0, 0]);
}
}
