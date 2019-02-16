<template>
  <div>
    <v-toolbar color="primary" dark fixed app>
      <!-- <v-toolbar-side-icon @click.stop="visible = !visible"></v-toolbar-side-icon> -->
      <v-toolbar-title>Portfolio</v-toolbar-title>
    </v-toolbar>
    <!-- <v-navigation-drawer
      v-model="visible"
      fixed
      app
    >
      <v-list dense>
        <v-list-tile
          v-for="(navigation, index) in navigations"
          :key="index"
          :to="navigation.route"
        >
          <v-list-tile-action>
            <v-icon>{{ navigation.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>{{ navigation.name }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer> -->
  </div>
</template>

<script>
export default {
  data: () => ({
    visible: false,
    navigations: [
      { icon: 'account_circle', name: 'ポートフォリオ', route: { name: 'portfolio' } },
      { icon: 'edit', name: 'プロフィール編集', route: { name: 'profile' } }
    ]
  })
}
</script>

<style>

</style>
