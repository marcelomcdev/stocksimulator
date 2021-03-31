import { NgbButtonLabel } from "@ng-bootstrap/ng-bootstrap";

export interface ModalConfig{
  modalTitle: string
  dismissButtonLabel?: string
  closeButtonLabel?: string
  shouldClose?(): Promise<boolean> | boolean
  shouldDismiss?(): Promise<boolean> |boolean
  onClose?(): Promise<boolean> |boolean
  onDismiss?(): Promise<boolean> |boolean
  disableCloseButton?(): boolean
  disableDismissButton?(): boolean
  hideCloseButton?(): NgbButtonLabel
  hideDismissButton?(): boolean
}
