package handlers

import (
	"data-store/internal/models"
	"github.com/rs/zerolog"
)

type IService interface {
	GetData(user models.User)
}

type Handler struct {
	logger  zerolog.Logger
	service IService
}

func New(logger zerolog.Logger, svc IService) *Handler {
	return &Handler{
		logger:  logger,
		service: svc,
	}
}
