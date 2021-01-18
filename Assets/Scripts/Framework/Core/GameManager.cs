using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HolographicKiller.Framework.Core
{
    public class GameManager : MonoBehaviour
    {
        private enum GameState
        {
            InUI,
            InGame,
            LostTracking
        }

        private GameState m_CurrentGameState = GameState.InUI;

        public static GameManager s_Instance;

        private GameState CurrentGameState
        {
            get
            {
                return m_CurrentGameState;
            }

            set
            {
                m_CurrentGameState = value;
            }
        }

        void Awake()
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}