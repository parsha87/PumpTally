import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddsaleComponent } from './addsale.component';

describe('AddsaleComponent', () => {
  let component: AddsaleComponent;
  let fixture: ComponentFixture<AddsaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddsaleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddsaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
