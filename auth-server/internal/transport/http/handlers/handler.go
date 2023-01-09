package handlers

import (
	"auth_server/internal/models"
	"github.com/rs/zerolog"
)

type IService interface {
	Login(user models.User) (interface{}, error)
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
