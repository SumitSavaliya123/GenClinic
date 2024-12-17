import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  constructor(private toastr: ToastrService) {}

  success(message: string, action?: string) {
    this.toastr.success(message, action, {
      timeOut: 1000,
      positionClass: 'toast-top-right',
    });
  }
  error(message: string, action?: string) {
    this.toastr.error(message, action, {
      timeOut: 3000,
      positionClass: 'toast-top-right',
    });
  }
  info(message: string, action?: string) {
    this.toastr.info(message, action, {
      timeOut: 3000,
      positionClass: 'toast-top-right',
    });
  }
  warning(message: string, action?: string) {
    this.toastr.warning(message, action, {
      timeOut: 3000,
      positionClass: 'toast-top-right',
    });
  }
}
