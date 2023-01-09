package service

import (
	"Messenger/internal/models"
	"fmt"
)

func (s *service) SomePay(receiver models.Receiver) string {
	bank := &models.BankPaymentHandler{}
	btc := &models.BTCTransferHandler{}
	paypal := &models.PayPalTransferHandler{}

	bank.Payment.Successor = paypal
	paypal.Payment.Successor = btc

	fmt.Println(bank, paypal.Payment)

	return bank.Handle(receiver)
}
