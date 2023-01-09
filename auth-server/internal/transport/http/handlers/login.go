package handlers

import (
	"auth_server/internal/models"
	"github.com/labstack/echo/v4"
	"net/http"
)

func (h *Handler) Login(c echo.Context) error {
	user := models.User{}

	if err := c.Bind(&user); err != nil {
		return err
	}

	data, err := h.service.Login(user)
	if err != nil {
		return err
	}

	return c.JSON(http.StatusOK, data)
}
