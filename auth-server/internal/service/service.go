package service

import (
	"auth_server/internal/models"
	"github.com/rs/zerolog"
)

type service struct {
	logger zerolog.Logger
}

func New(logger zerolog.Logger) *service {
	return &service{
		logger: logger,
	}
}

func (s *service) Login(user models.User) (interface{}, error) {
	return nil, nil
}
