package handlers

import (
	"data-store/internal/models"
	"github.com/labstack/echo/v4"
)

func (h *Handler) UserData(c echo.Context) error {
	user := models.User{}

	if err := c.Bind(&user); err != nil {
		return err
	}

	h.service.GetData(user)

	return nil
}
