package models

import (
	"fmt"
)

type (
	IPaymentHandler interface {
		Handle(receiver Receiver) string
	}

	Receiver struct {
		BankTransfer   bool `json:"bank_transfer"`
		BTCTransfer    bool `json:"btc_transfer"`
		PayPalTransfer bool `json:"paypal_transfer"`
	}

	Payment struct {
		Successor IPaymentHandler
	}

	BankPaymentHandler struct {
		Payment Payment
	}

	BTCTransferHandler struct {
		Payment Payment
	}

	PayPalTransferHandler struct {
		Payment Payment
	}
)

func (e *BankPaymentHandler) Handle(receiver Receiver) string {
	fmt.Println(receiver.BankTransfer, receiver.BTCTransfer, receiver.PayPalTransfer)
	if receiver.BankTransfer {
		return "выполняем банковский перевод"
	} else if e.Payment.Successor != nil {
		return e.Payment.Successor.Handle(receiver)
	}

	return "ошибка перевода"
}

func (e BTCTransferHandler) Handle(receiver Receiver) string {
	fmt.Println("btc")
	if receiver.BTCTransfer {
		return "выполняем перевод через биткоины"
	} else if e.Payment.Successor != nil {
		return e.Payment.Successor.Handle(receiver)
	}

	return "ошибка перевода"
}

func (e PayPalTransferHandler) Handle(receiver Receiver) string {
	if receiver.PayPalTransfer {
		fmt.Println("перевод пейпалами")
		return "перевод через paypal"
	} else if e.Payment.Successor != nil {
		return e.Payment.Successor.Handle(receiver)
	}

	return "ошибка перевода"
}
