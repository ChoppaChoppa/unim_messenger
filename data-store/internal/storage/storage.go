package storage

import (
	"data-store/internal/models"
	"github.com/jackc/pgx/v4/pgxpool"
)

type storage struct {
	pool *pgxpool.Pool
}

func New(pool *pgxpool.Pool) *storage {
	return &storage{
		pool: pool,
	}
}

func (s *storage) GetUserData(user models.User) ([]*models.UserData, error) {
	return nil, nil
}
