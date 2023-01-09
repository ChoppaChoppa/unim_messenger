package handler

import (
	"Messenger/internal/models"
	"fmt"
	"github.com/labstack/echo/v4"
	"net/http"
)

func (h *Handler) SomePay(c echo.Context) error {
	receiver := models.Receiver{}
	if err := c.Bind(&receiver); err != nil {
		return err
	}
	fmt.Println(receiver.BankTransfer, receiver.BTCTransfer, receiver.PayPalTransfer)
	str := h.service.SomePay(receiver)
	return c.String(http.StatusOK, str)
}
