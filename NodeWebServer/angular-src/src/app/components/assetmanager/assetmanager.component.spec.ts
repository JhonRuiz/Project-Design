import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AssetmanagerComponent } from './assetmanager.component';

describe('AssetmanagerComponent', () => {
  let component: AssetmanagerComponent;
  let fixture: ComponentFixture<AssetmanagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssetmanagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssetmanagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
