package handler

import (
	"Messenger/internal/models"
	"github.com/labstack/echo/v4"
	"net/http"
)

func (h *Handler) StudentJson(c echo.Context) error {
	serialize := models.JsonSerializer{}
	acc := models.Student{}

	json := h.service.Accept(serialize, acc)

	return c.String(200, json)
}

func (h *Handler) TeacherJson(c echo.Context) error {
	serialize := models.JsonSerializer{}
	acc := models.Teacher{}

	json := h.service.Accept(serialize, acc)

	return c.String(http.StatusOK, json)
}

func (h *Handler) StudentXml(c echo.Context) error {
	serialize := models.XmlSerializer{}
	acc := models.Student{}

	xml := h.service.Accept(serialize, acc)

	return c.String(http.StatusOK, xml)
}

func (h *Handler) TeacherXml(c echo.Context) error {
	serialize := models.XmlSerializer{}
	acc := models.Teacher{}

	xml := h.service.Accept(serialize, acc)

	return c.String(http.StatusOK, xml)
}
