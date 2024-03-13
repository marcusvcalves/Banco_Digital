import { CreditCard } from "./CreditCard"

export type Policy = {
    id: number,
    number: string,
    hiringDate: string,
    value: number,
    triggeringDescription: string,
    creditCardId: number,
    creditCard: CreditCard
  }