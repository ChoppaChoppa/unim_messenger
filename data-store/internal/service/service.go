package service

import (
	"data-store/internal/models"
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

func (s *service) GetData(user models.User) {

}
