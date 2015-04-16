using UnityEngine;
using System.Collections;
using com.gamehound.broops.model.datamodel;

namespace com.gamehound.broops.model
{
    public enum EnemyModelType
    {
        TestEnemy
    }

    public static class EnemyFactory
    {

        public static EnemyModelBase CreateEnemy(EnemyModelType enemy)
        {
            EnemyModelBase toConfigure;

            switch (enemy)
            {
                default:
                    toConfigure = new EnemyTestModel();
                    break;
            }

            return toConfigure;
        }
    }
}
